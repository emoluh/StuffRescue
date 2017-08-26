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
        this.gotoDetail();
    }

    gotoDetail(): void {
        this.router.navigate(['/detail', this.selectedFeature.featureId]);
    }


    gotoAdd(): void {
        this.router.navigate(['/Add', this.selectedFeature.featureId]);
    }

    delete(feature: Feature): void {
        this.featureService
            .delete(feature.featureId)
            .then(() => {
                this.features = this.features.filter(h => h !== feature);
                if (this.selectedFeature === feature) { this.selectedFeature = null; }
            });
    }
}
