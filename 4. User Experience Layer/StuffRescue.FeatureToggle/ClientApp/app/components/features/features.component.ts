import { Component, OnInit } from '@angular/core';
import { Feature } from '../shared/feature';

import { FeatureService } from '../shared/feature.service';
import { Router } from '@angular/router';


@Component({
    selector: 'my-features',
    templateUrl: './features.component.html',
    styleUrls: ['./features.component.css'],
    providers: [
        FeatureService
    ]
})
export class FeaturesComponent implements OnInit{
    features: Feature[];
    selectedFeature: Feature;

    constructor(
        private router: Router,
        private featureService: FeatureService) { }

    getFeatures(): void {
        this.featureService.getFeatures().then(features => this.features = features);
    }

    ngOnInit(): void {
        this.getFeatures();
    }

    onSelect(feature: Feature): void {
        this.selectedFeature = feature;
    }

    delete(feature: Feature): void {
        //TODO: Implement Delete operation
        //this.featureService
        //    .delete(feature.id)
        //    .then(() => {
        //        this.features = this.features.filter(h => h !== feature);
        //        if (this.selectedFeature === feature) { this.selectedFeature = null; }
        //    });
        alert(feature.name + " is deleted");
    }
}
