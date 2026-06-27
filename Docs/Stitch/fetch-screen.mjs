import { stitch } from "@google/stitch-sdk";
import fs from "fs";
import path from "path";
import { fileURLToPath } from "url";

const __dirname = path.dirname(fileURLToPath(import.meta.url));
const projectId = "13162339874056363525";
const screenId = "8a1ef21c68f54f98b110ba2bdbd42659";
const outDir = path.join(__dirname, projectId);

fs.mkdirSync(outDir, { recursive: true });

const project = stitch.project(projectId);
const screen = await project.getScreen(screenId);

const htmlUrl = await screen.getHtml();
const imageUrl = await screen.getImage();

console.log("HTML URL:", htmlUrl);
console.log("Image URL:", imageUrl);

const meta = {
  projectId,
  screenId,
  title: screen.title ?? "Admin Dashboard - Modern WinForms Style",
  htmlUrl,
  imageUrl,
  fetchedAt: new Date().toISOString(),
};
fs.writeFileSync(path.join(outDir, "screen-meta.json"), JSON.stringify(meta, null, 2));

async function download(url, filename) {
  const res = await fetch(url);
  if (!res.ok) throw new Error(`Failed ${filename}: ${res.status}`);
  const buf = Buffer.from(await res.arrayBuffer());
  fs.writeFileSync(path.join(outDir, filename), buf);
  console.log("Saved", filename, buf.length, "bytes");
}

await download(htmlUrl, "admin-dashboard.html");
await download(imageUrl, "admin-dashboard.png");

console.log("Done.");
