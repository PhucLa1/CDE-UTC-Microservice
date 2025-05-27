
"use client";

import View3D from "../project/[id]/view-file/_components/view-3d";

export default function page() {
  return (
    <>
      <View3D fileUrl="https://localhost:5052/office_building/scene.gltf" fileType="gltf" />
    </>
  );
}
