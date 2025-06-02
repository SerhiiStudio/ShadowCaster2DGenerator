# ShadowCaster2D Generator for Unity

> Automatic generator for ShadowCaster2D components based on sprite alpha channels. Suitable for creating complex 2D shadows in the Universal Render Pipeline.

## Links:
- [Key Features](#-key-features)
- [Usage](#-usage)
- [Structure](#-structure)
- [Notes](#%EF%B8%8F-notes)
- [Author](#-author)

---
## 🔧 Key Features

- Generates `ShadowCaster2D` components based on sprite outlines.
- Determines shadow shape considering transparency (alpha channel).
- Saves created objects as prefabs to a specified folder.
- Cleans up temporary objects from the scene.
- Visualizes contours in the editor via Gizmos.
- **Enhanced Efficiency & Advanced Automation:**
	- **Dramatically boosts productivity** by transforming 1-5 hours of manual effort into a swift 30-second to 2-minute automated process.
	- **Utilizes sophisticated algorithms** for **alpha channel-based pixel traversal and contour mask creation**, followed by precise contour tracing and coordinate conversion. This solution was developed with **AI guidance**, demonstrating an innovative approach to complex automation challenges.
---

## 📦 Usage

1. Add the `ShadowCaster2DMaker` component to a `GameObject` in your scene.
2. Populate the sprites list, select the folder, and specify the `targetFolderName`.
3. Call `GenerateShadows()` from the Inspector or via an editor script.
4. If needed, call `ClearMadeGameObjects()` to remove temporary objects.  
> To generate shadows without saving them to a folder, activate the **DoNotSaveToFolder** flag in the **Advanced** menu.   
⚠️ Notes: 
> - If you enable **DoNotSaveToFolder** during generation and then disable it, the generated objects won't be saved to the folder, and `ClearMadeGameObjects` will no longer have a reference to them.
> - `ClearMadeGameObjects` will remove all objects created in the current session from the scene, provided it still holds references to them (see above).

---

## 📁 Structure

### `ShadowCaster2DMaker.cs`

A component for generating shadows from sprites in the editor.

#### Fields:
- `Sprite[] sprites` — An array of sprites for generation.
- `DefaultAsset folder` — The root folder for saving the results.
- `string targetFolderName` — The name of the subfolder for the output.
- Advanced:
	- `bool doNotSaveToFolder` — A flag to prevent saving as prefabs.

#### Methods:
- `GenerateShadows()` —  The main method for generating shadows.
- `ClearMadeGameObjects()` — Cleans up created objects from the scene (if they were saved as prefabs or cleared manually).

---

### `MakePoints.cs`

A helper static class for processing sprites.

#### Key Steps:
1. **GetPixels** — Retrieves pixels from the sprite's texture.
2. **GetAlphaMask** — Creates a 2D mask of opaque pixels.
3. **ExtractContourMask** — Extracts the contour based on the mask.
4. **ExtractContourPoints** — Forms a sequence of points along the contour.
5. **SimplifyPoints** — Simplifies the geometry (removes redundant points).
6. **ConvertToLocalPoints** — Converts points to local coordinates.

#### Result:
A `Vector3[]` array of points for the `m_ShapePath` field of the `ShadowCaster2D` component.

---

### `VisualizeShadow.cs`

A Gizmos component for displaying the shadow contour in the editor.

#### Fields:
- `ShadowCaster2D shadowCaster` — A reference to the caster.

#### Features:
- Draws Gizmos lines between the points of the `m_ShapePath` private field of the `ShadowCaster2D` component.

---

### `SaveToFolder.cs`

A helper static class for saving shadows as prefabs.

#### Fields:
- `const string PREFAB_EXTENSION` - The string `".prefab"` used to construct the path before saving.

#### Features:
- Determines the save path.
- Saves a `GameObject` as a `.prefab` file.

---

## ⚠️ Notes

- The scripts use reflection to access private fields and methods of `ShadowCaster2D` and `ShadowUtility`.
- Tested with `
Unity 2022.3.35f1` and `URP 14.0.11`, may not work or might cause errors with other Unity versions.
- Works only in the **Unity Editor**, as it utilizes `UnityEditor`, `Undo`, `EditorUtility`, etc.
- It is assumed that Universal Render Pipeline (URP) is already integrated into the project.

---

## 👨‍💻 Author

**SerhiiStudio**  
Unity Tools & Games  
GitHub: _[SerhiiStudio](https://github.com/SerhiiStudio)_  
