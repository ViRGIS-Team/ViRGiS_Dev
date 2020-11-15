using UnityEngine;
using System.Threading.Tasks;
using Project;

namespace Virgis {

    public class MapBoxLayer : VirgisLayer<RecordSet, object> {

        private void Start() {
            featureType = FeatureType.NONE;
        }

        protected override async Task _init() {
            int a = 1;
        }

        protected override VirgisFeature _addFeature(Vector3[] geometry) {
            throw new System.NotImplementedException();
        }

        protected override void _checkpoint() {
            //do nothing
        }

        protected override void _draw() {
            // do nothing
        }

        protected override Task _save() {
            return Task.CompletedTask;
        }
    }
}


