import { stitch } from "@google/stitch-sdk";
import fs from "fs";
import path from "path";
import { fileURLToPath } from "url";

const __dirname = path.dirname(fileURLToPath(import.meta.url));
const projectId = "13162339874056363525";
const screenId = "06c9bb2c27c3420f982ee8f84ca605c3";
const outDir = path.join(__dirname, projectId);

fs.mkdirSync(outDir, { recursive: true });

const project = stitch.project(projectId);
const screen = await project.getScreen(screenId);

const htmlUrl = await screen.getHtml();
const imageUrl = await screen.getImage();

console.log("Title:", screen.title);
console.log("HTML URL:", htmlUrl);
console.log("Image URL:", imageUrl);

const meta = {
  projectId,
  screenId,
  title: screen.title ?? "Navigation Reference - Admin & Customer",
  htmlUrl,
  imageUrl,
  fetchedAt: new Date().toISOString(),
};
fs.writeFileSync(path.join(outDir, "navigation-reference-meta.json"), JSON.stringify(meta, null, 2));

async function download(url, filename) {
  const res = await fetch(url);
  if (!res.ok) throw new Error(`Failed ${filename}: ${res.status}`);
  const buf = Buffer.from(await res.arrayBuffer());
  fs.writeFileSync(path.join(outDir, filename), buf);
  console.log("Saved", filename, buf.length, "bytes");
}

await download(htmlUrl, "navigation-reference.html");
await download(imageUrl, "navigation-reference.png");

console.log("Done.");
