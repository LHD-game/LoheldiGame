using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace TileMap3D
{
    /// <summary>
    /// The Combiner Class is used to combine different meshes into one, setting meshes with different materials as their own sub-mesh.
    /// </summary>
    [RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
    public class Combiner : MonoBehaviour
    {
        #region Variables
        //A list for the mesh filters of the objects that will be combined
        private List<MeshFilter> _filterList = new List<MeshFilter>(); 
        private CombineInstance[] combine; //The combine instances that will be used to combine the meshes
        //A list of all the different materials that the meshes have
        private List<Material> difMaterials = new List<Material>(); 
        #endregion

        #region Functions
        private void GetHierarchy(GameObject obj) //Get the Mesh Filter from every object and their child objects
        {
            foreach (Transform child in obj.transform)
            {
                //Get the Mesh Filter and Mesh Renderer from the object./////////
                MeshFilter mf = child.gameObject.GetComponent<MeshFilter>();
                MeshRenderer mr = child.gameObject.GetComponent<MeshRenderer>();
                /////////////////////////////////////////////////////////////////
                if (mf != null && mf.sharedMesh != null)
                { //If the object has a both components
                    _filterList.Add(mf); //Add the mesh filter to the mesh filter list
                    if (difMaterials.Count == 0) //Only for the first material
                    { //Add the material to the list of different materials found on the level
                        difMaterials.Add(mf.gameObject.GetComponent<MeshRenderer>().sharedMaterial);
                    }
                    else
                    {
                        bool match = false;
                        foreach (Material mat in difMaterials)
                        { //Check every material to see if the current object has different one
                            if (mr.sharedMaterial == mat)
                            {
                                match = true;
                                break;
                            }
                        }
                        if (match == false)
                        { //In case the material has not been saved, then add it to the materials list
                            difMaterials.Add(mr.sharedMaterial);
                        }
                    }
                }
                //Repeat the process for every child object
                GetHierarchy(child.gameObject);
            }
            //After getting every mesh filter from every object and its children objects Start combining the meshes
            CombineArea();
        }

        public void GetHierarchy(GameObject[] objects) //Get the Mesh Filter from every object and their child objects
        {
            foreach (GameObject obj in objects)
            {
                GetMesh(obj.transform);
                foreach (Transform child in obj.transform)
                {
                    GetMesh(child);
                    //Repeat the process for every child object
                    GetHierarchy(child.gameObject);
                }
                obj.SetActive(false);
            }
            //After getting every mesh filter from every object and its children objects Start combining the meshes
            CombineArea();
        }

        private void GetMesh(Transform target)
        {
            //Get the Mesh Filter and Mesh Renderer from the object./////////
            MeshFilter mf = target.gameObject.GetComponent<MeshFilter>();
            MeshRenderer mr = target.gameObject.GetComponent<MeshRenderer>();
            /////////////////////////////////////////////////////////////////
            if (mf != null && mf.sharedMesh != null)
            { //If the object has a both components
                _filterList.Add(mf); //Add the mesh filter to the mesh filter list
                if (difMaterials.Count == 0) //Only for the first material
                { //Add the material to the list of different materials found on the level
                    difMaterials.Add(mf.gameObject.GetComponent<MeshRenderer>().sharedMaterial);
                }
                else
                {
                    bool match = false;
                    foreach (Material mat in difMaterials)
                    { //Check every material to see if the current object has different one
                        if (mr.sharedMaterial == mat)
                        {
                            match = true;
                            break;
                        }
                    }
                    if (match == false)
                    { //In case the material has not been saved, then add it to the materials list
                        difMaterials.Add(mr.sharedMaterial);
                    }
                }
                target.gameObject.SetActive(false);
            }
        }

        public void CombineArea() //Combine the meshes into one mesh with a sub mesh for every different material
        {
            //Get the combiner object current position and save it for later, set the position as 0 to avoid any...
            //later problems with mesh positioning//////////////
            Vector3 position = gameObject.transform.position;
            gameObject.transform.position = Vector3.zero;
            ////////////////////////////////////////////////////
            List<Mesh> meshes = new List<Mesh>(); //Create a new list to store all the meshes from the mesh filters
            //Initialize the Combine Instance array with one space for every different material
            combine = new CombineInstance[difMaterials.Count]; 
            //Make a new mesh for every different material encountered on the meshes containing only the meshes with...
            //each material
            for (int j = 0; j < difMaterials.Count; j++) 
            {
                List<MeshFilter> filters = new List<MeshFilter>();
                //Check every mesh on the mesh filter list to see if it has the currently checked material and if so...
                //add it to a new Mesh Filter List//////////////////////////////////////////////
                for (int k = 0; k < _filterList.Count; k++)
                {
                    MeshRenderer mr = _filterList[k].gameObject.GetComponent<MeshRenderer>();
                    if (mr.sharedMaterial == difMaterials[j])
                    {
                        filters.Add(_filterList[k]);
                    }
                }
                /////////////////////////////////////////////////////////////////////////////////
                CombineInstance[] combineInst = new CombineInstance[filters.Count]; //Create a secondary Combine instance array
                int l = 0;
                while (l < filters.Count)
                { //Get the components needed by the  secondary Combine Instance array from the meshes from each material
                    combineInst[l].mesh = filters[l].sharedMesh;
                    combineInst[l].transform = filters[l].transform.localToWorldMatrix;
                    combineInst[l].subMeshIndex = 0;
                    l++;   
                }
                meshes.Add(new Mesh()); //Create a new mesh and add it to the meshes list
                meshes[j].CombineMeshes(combineInst,true,true); //Combine the gathered meshes and set them as the added mesh

                //Get the new mesh's information for the main Combine Instance Array
                combine[j].mesh = meshes[j];
                combine[j].subMeshIndex = 0;
                
            }
            transform.GetComponent<MeshFilter>().sharedMesh = new Mesh(); //Create a new mesh for the Combiner's Mesh Filter
            //Combine the main Combine Instance Array with the material meshes into a new mesh and set it as the game object's mesh
            transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine,false,false);
            //Get a reference to the final mesh
            Mesh mesh = transform.GetComponent<MeshFilter>().sharedMesh;
            //Change the mesh's name 
            mesh.name = "MOD_" + gameObject.name;
            //Create a new material array with a space for each found material
            Material[] materials = new Material[difMaterials.Count];
            for (int i = 0; i < difMaterials.Count; i++) //Reference the found materials on the array
            {
                materials[i] = difMaterials[i];
            }
            //Set the found materials as the final mesh's materials
            transform.GetComponent<MeshRenderer>().sharedMaterials = materials;
            //Set the Combiner's Game Object as active
            transform.gameObject.SetActive(true);
            //Reset the Combiner's position to its original position
            gameObject.transform.position = position;
        }
        #endregion
    }
}
