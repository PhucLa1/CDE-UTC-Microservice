import { useEffect, useRef, useState } from 'react';
import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader';
import { OBJLoader } from 'three/examples/jsm/loaders/OBJLoader';
import { FBXLoader } from 'three/examples/jsm/loaders/FBXLoader';

interface View3DProps {
  fileUrl: string;
  fileType: 'gltf' | 'glb' | 'obj' | 'fbx';
}

const View3D: React.FC<View3DProps> = ({ fileUrl, fileType }) => {
  const mountRef = useRef<HTMLDivElement>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    if (!mountRef.current) return;

    // Thiết lập scene
    const scene = new THREE.Scene();
    scene.background = new THREE.Color(0x1e293b);

    // Thiết lập camera, vị trí camera
    const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
    camera.position.set(10, 10, 10);

    // Renderer
    const renderer = new THREE.WebGLRenderer({ antialias: true });
    renderer.setSize(window.innerWidth, window.innerHeight);
    renderer.setPixelRatio(window.devicePixelRatio);
    mountRef.current.appendChild(renderer.domElement);

    // Lighting
    const ambientLight = new THREE.AmbientLight(0xffffff, 0.9);
    scene.add(ambientLight);
    const directionalLight = new THREE.DirectionalLight(0xffffff, 0.8);
    directionalLight.position.set(10, 10, 10);
    scene.add(directionalLight);

    // Load model based on file type
    const loadModel = () => {
      let loader;
      if (fileType === 'gltf' || fileType === 'glb') {
        loader = new GLTFLoader();
        loader.load(
          fileUrl,
          (gltf: any) => {
            scene.add(gltf.scene);
            centerAndScale(gltf.scene);
            setLoading(false);
          },
          undefined,
          (error: any) => {
            console.error('Error loading glTF/GLB:', error);
            setLoading(false);
          }
        );
      } else if (fileType === 'obj') {
        loader = new OBJLoader();
        loader.load(
          fileUrl,
          (obj: any) => {
            scene.add(obj);
            centerAndScale(obj);
            setLoading(false);
          },
          undefined,
          (error: any) => {
            console.error('Error loading OBJ:', error);
            setLoading(false);
          }
        );
      } else if (fileType === 'fbx') {
        loader = new FBXLoader();
        loader.load(
          fileUrl,
          (fbx: any) => {
            scene.add(fbx);
            centerAndScale(fbx);
            setLoading(false);
          },
          undefined,
          (error: any) => {
            console.error('Error loading FBX:', error);
            setLoading(false);
          }
        );
      }
    };

    // Center and scale model
    const centerAndScale = (object: THREE.Object3D) => {
      const box = new THREE.Box3().setFromObject(object);
      const center = box.getCenter(new THREE.Vector3());
      object.position.sub(center);
      const size = box.getSize(new THREE.Vector3()).length();
      camera.position.set(size, size, size);
    };

    loadModel();

    // Orbit controls for interaction
    const controls = new OrbitControls(camera, renderer.domElement);
    controls.enableDamping = true;
    controls.dampingFactor = 0.05;
    controls.minDistance = 1;
    controls.maxDistance = 200;

    // Animation loop
    const animate = () => {
      requestAnimationFrame(animate);
      controls.update();
      renderer.render(scene, camera);
    };
    animate();

    // Handle window resize
    const handleResize = () => {
      camera.aspect = window.innerWidth / window.innerHeight;
      camera.updateProjectionMatrix();
      renderer.setSize(window.innerWidth, window.innerHeight);
    };
    window.addEventListener('resize', handleResize);

    // Handle reset camera
    const handleReset = () => {
      camera.position.set(10, 10, 10);
      controls.reset();
    };
    window.addEventListener('resetCamera', handleReset);

    // Cleanup
    return () => {
      window.removeEventListener('resize', handleResize);
      window.removeEventListener('resetCamera', handleReset);
      if (mountRef.current) {
        mountRef.current.removeChild(renderer.domElement);
      }
    };
  }, [fileUrl, fileType]);

  return (
    <div ref={mountRef} className="w-full h-screen relative">
      {loading && (
        <div className="absolute inset-0 flex items-center justify-center text-white bg-black bg-opacity-50">
          Loading...
        </div>
      )}
    </div>
  );
};

export default View3D;