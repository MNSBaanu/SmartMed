import { stitch } from "@google/stitch-sdk";
import fs from "fs";
import path from "path";
import { fileURLToPath } from "url";

const __dirname = path.dirname(fileURLToPath(import.meta.url));
const projectId = "13162339874056363525";
const screenId = "719984e1cd6e4c5cb8b3f16793014c49";
const outDir = path.join(__dirname, projectId);

fs.mkdirSync(outDir, { recursive: true });

const project = stitch.project(projectId);
const screen = await project.getScreen(screenId);
const htmlUrl = await screen.getHtml();
const imageUrl = await screen.getImage();

console.log("Title:", screen.title);
console.log("HTML URL:", htmlUrl);
console.log("Image URL:", imageUrl);

async function download(url, filename) {
  const res = await fetch(url);
  if (!res.ok) throw new Error(`Failed ${filename}: ${res.status}`);
  const buf = Buffer.from(await res.arrayBuffer());
  fs.writeFileSync(path.join(outDir, filename), buf);
  console.log("Saved", filename, buf.length, "bytes");
  return buf.length;
}

const htmlBytes = await download(htmlUrl, "registration.html");
const pngBytes = await download(imageUrl, "registration.png");

const meta = {
  projectId,
  screenId,
  title: screen.title ?? "Registration - Modern WinForms Style",
  htmlUrl,
  imageUrl,
  htmlFile: "registration.html",
  pngFile: "registration.png",
  htmlBytes,
  pngBytes,
  fetchedAt: new Date().toISOString(),
};
fs.writeFileSync(path.join(outDir, "registration-meta.json"), JSON.stringify(meta, null, 2));
console.log("Done.");
