
using UnityEngine;
using Virgis;
using Project;
using System.Threading.Tasks;
using Mapbox.Unity.Map;
using System;

public  class MapInit : MapInitialize{

    //References to the Prefabs to be used for Layers
    public GameObject MapBoxLayer;
    public GameObject VectorLayer;
    public GameObject RasterLayer;
    public GameObject PointCloud;
    public GameObject MeshLayer;
    public GameObject MDALLayer;
    public GameObject DemLayer;


    public override async Task<VirgisLayer> createLayer(RecordSet thisLayer){
        VirgisLayer temp = null;  
        switch (thisLayer.DataType){
            case RecordSetDataType.MapBox:
                GeogData props = thisLayer.Properties;
                VirgisAbstractMap mbLayer = Instantiate(MapBoxLayer, transform).GetComponent<VirgisAbstractMap>();
                mbLayer.UseWorldScale();
                ImagerySourceType st;
                ElevationLayerType lt;
                ElevationSourceType et;
                if (Enum.TryParse<ImagerySourceType>(props.imagerySourceType, true, out st) &&
                    Enum.TryParse<ElevationLayerType>(props.elevationLayerType, true, out lt) &&
                    Enum.TryParse<ElevationSourceType>(props.elevationSourceType, true, out et))
                {
                    mbLayer.SetProperties(st, lt, et, props.MapSize);
                }
                mbLayer.Initialize(appState.project.Origin.Coordinates.Vector2d(), props.MapScale);
                temp = mbLayer.GetComponent<MapBoxLayer>();
                temp.SetMetadata(thisLayer);
                temp.changed = false;
                break;
            case RecordSetDataType.Vector:
                temp = await Instantiate(VectorLayer, transform).GetComponent<OgrLayer>().Init(thisLayer);
                break;
            case RecordSetDataType.Raster:
                temp = await Instantiate(RasterLayer, transform).GetComponent<RasterLayer>().Init(thisLayer as RecordSet);
                break;
            case RecordSetDataType.PointCloud:
                temp = await Instantiate(PointCloud, transform).GetComponent<PointCloudLayer>().Init(thisLayer as RecordSet);
                break;
            case RecordSetDataType.Mesh:
                temp = await Instantiate(MeshLayer, transform).GetComponent<MeshLayer>().Init(thisLayer as RecordSet);
                break;
            case RecordSetDataType.Mdal:
                temp = await Instantiate(MDALLayer, transform).GetComponent<MdalLayer>().Init(thisLayer as RecordSet);
                break;
            case RecordSetDataType.DEM:
                temp = await Instantiate(DemLayer, transform).GetComponent<DemLayer>().Init(thisLayer as RecordSet);
                break;
            default:
                Debug.LogError(thisLayer.DataType.ToString() + " is not known.");
                break;
            }
        return temp;
    }
}

