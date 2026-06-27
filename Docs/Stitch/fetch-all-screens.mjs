import { stitch } from "@google/stitch-sdk";
import fs from "fs";
import path from "path";
import { fileURLToPath } from "url";

const __dirname = path.dirname(fileURLToPath(import.meta.url));
const projectId = "13162339874056363525";

const screens = [
  { id: "97fc709c2569442aa203bb02a1340d9c", slug: "my-orders" },
  { id: "8158597c7b814767ab9b09f4e029de3e", slug: "reports-analytics" },
  { id: "7e936a0aaed44267b68855ebfced5101", slug: "manage-orders" },
  { id: "6a4f5607492049b28347c238c87bd5a3", slug: "my-cart" },
  { id: "c46c137330e64465a9df2292674547b1", slug: "manage-customers" },
  { id: "fd5b56f4515b4199bb8070662bdc5b21", slug: "manage-inventory" },
  { id: "8a1ef21c68f54f98b110ba2bdbd42659", slug: "admin-dashboard" },
  { id: "7cf550b09037449a809c015b1f1c96cd", slug: "browse-medicine" },
  { id: "d707d5003a8745ecbc5edf8be7d86af6", slug: "my-profile" },
  { id: "f7007caf4e6a48d1962ce2e95ddee7b8", slug: "home" },
];

const outDir = path.join(__dirname, projectId);
fs.mkdirSync(outDir, { recursive: true });

const project = stitch.project(projectId);
const index = [];

async function download(url, filePath) {
  const res = await fetch(url);
  if (!res.ok) throw new Error(`Failed ${filePath}: ${res.status}`);
  const buf = Buffer.from(await res.arrayBuffer());
  fs.writeFileSync(filePath, buf);
  return buf.length;
}

for (const spec of screens) {
  console.log(`Fetching ${spec.slug} (${spec.id})...`);
  try {
    const screen = await project.getScreen(spec.id);
    const htmlUrl = await screen.getHtml();
    const imageUrl = await screen.getImage();
    const htmlPath = path.join(outDir, `${spec.slug}.html`);
    const pngPath = path.join(outDir, `${spec.slug}.png`);
    const htmlBytes = await download(htmlUrl, htmlPath);
    const pngBytes = await download(imageUrl, pngPath);
    index.push({
      slug: spec.slug,
      screenId: spec.id,
      title: screen.title ?? spec.slug,
      htmlUrl,
      imageUrl,
      htmlFile: `${spec.slug}.html`,
      pngFile: `${spec.slug}.png`,
      htmlBytes,
      pngBytes,
    });
    console.log(`  OK ${spec.slug} (${htmlBytes} + ${pngBytes} bytes)`);
  } catch (err) {
    console.error(`  FAIL ${spec.slug}:`, err.message);
    index.push({ slug: spec.slug, screenId: spec.id, error: err.message });
  }
}

index.push({ projectId, fetchedAt: new Date().toISOString() });
fs.writeFileSync(path.join(outDir, "screens-index.json"), JSON.stringify(index, null, 2));
console.log("Done. Index:", path.join(outDir, "screens-index.json"));
