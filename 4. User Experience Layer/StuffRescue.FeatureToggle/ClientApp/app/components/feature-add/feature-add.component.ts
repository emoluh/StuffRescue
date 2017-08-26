import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import { Feature } from '../shared/feature';
import { FeatureService } from '../shared/feature.service';

@Component({
    selector: 'feature-add',
    templateUrl: './feature-add.component.html',
    styleUrls: ['./feature-add.component.css'],
    providers: [
        FeatureService
    ]
})
export class FeatureAddComponent implements OnInit{
    constructor(
        private featureService: FeatureService,
        private location: Location) { }

    getFeatures(): void {
        this.featureService.getFeatures().then();
    }

    ngOnInit(): void {
        this.getFeatures();
    }


    goBack(): void {
        this.location.back();
    }

    add(name: string): void {
        name = name.trim();
        if (!name) { return; }
        this.featureService.create(name)
            .then(() => this.goBack());
    }
}
