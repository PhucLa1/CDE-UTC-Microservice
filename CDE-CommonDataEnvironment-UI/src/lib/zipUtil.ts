import JSZip from "jszip";

export interface FileItem {
  name: string;
  url: string;
}

export interface FolderItem {
  name: string;
  files: FileItem[];
  subFolders: FolderItem[];
}

/**
 * Tải file từ URL và thêm vào folder trong JSZip
 */
const fetchAndAddFile = async (zipFolder: JSZip, file: FileItem) => {
  const response = await fetch(file.url);
  const blob = await response.blob();
  zipFolder.file(file.name, blob);
};

/**
 * Đệ quy xử lý folder và thêm vào JSZip
 */
const processFolder = async (zipFolder: JSZip, folder: FolderItem) => {
  const folderZip = zipFolder.folder(folder.name);

  // Thêm file vào folder
  const filePromises = folder.files.map(file => fetchAndAddFile(folderZip!, file));

  // Xử lý sub-folder
  const folderPromises = folder.subFolders.map(subFolder => processFolder(folderZip!, subFolder));

  await Promise.all([...filePromises, ...folderPromises]);
};

/**
 * Tải về danh sách folder dưới dạng file ZIP
 */
export const downloadFolderAsZip = async (folder: FolderItem, zipName: string) => {
  const zip = new JSZip();

  await processFolder(zip, folder); // Không cần dùng .map() vì chỉ có một folder

  const zipBlob = await zip.generateAsync({ type: "blob" });

  const url = URL.createObjectURL(zipBlob);
  const a = document.createElement("a");
  a.href = url;
  a.download = zipName;
  document.body.appendChild(a);
  a.click();
  URL.revokeObjectURL(url);
};

