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
export class FeatureAddComponent implements OnInit {
    state: boolean;

    constructor(
        private featureService: FeatureService,
        private location: Location) { }


    ngOnInit(): void {
        this.state = true;
    }

    onSelect(state: boolean): void {
        this.state = state;
    }

    goBack(): void {
        this.location.back();
    }

    add(name: string, state: boolean): void {
        name = name.trim();
        if (!name) { return; }
        this.featureService.create(name,state)
            .then(() => this.goBack());
    }
}
