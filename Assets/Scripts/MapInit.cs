
using UnityEngine;
using Virgis;
using Project;
using System;
using Mapbox.Unity.Map;


public class MapInit : MapInitialize
{

    //References to the Prefabs to be used for Layers
    public GameObject MapBoxLayer;
    public GameObject VectorLayer;
    public GameObject RasterLayer;
    public GameObject PointCloud;
    public GameObject MeshLayer;
    public GameObject MDALLayer;
    public GameObject DemLayer;
    public GameObject XSectLayer;
    public GameObject BoreHoleLayer;


    public override void onLoad()
    {
        // do nothing
    }

    public override VirgisLayer createLayer(RecordSet thisLayer)
    {
        VirgisLayer temp = null;
        switch (thisLayer.DataType)
        {
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
                temp = Instantiate(VectorLayer, transform).GetComponent<OgrLayer>();
                break;
            case RecordSetDataType.Raster:
                temp = Instantiate(RasterLayer, transform).GetComponent<RasterLayer>();
                break;
            case RecordSetDataType.PointCloud:
                temp = Instantiate(PointCloud, transform).GetComponent<PointCloudLayer>();
                break;
            case RecordSetDataType.Mesh:
                temp = Instantiate(MeshLayer, transform).GetComponent<MeshLayer>();
                break;
            case RecordSetDataType.Mdal:
                temp = Instantiate(MDALLayer, transform).GetComponent<MdalLayer>();
                break;
            case RecordSetDataType.DEM:
                temp = Instantiate(DemLayer, transform).GetComponent<DemLayer>();
                break;
            default:
                Debug.LogError(thisLayer.DataType.ToString() + " is not known.");
                break;
        }
        return temp;
    }
}

