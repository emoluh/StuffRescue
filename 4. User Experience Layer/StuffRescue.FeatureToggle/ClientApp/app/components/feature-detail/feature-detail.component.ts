import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Location } from '@angular/common';

import { Feature } from '../shared/feature';
import { FeatureService } from '../shared/feature.service';

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'feature-detail',
    templateUrl: './feature-detail.component.html',
    styleUrls: ['./feature-detail.component.css'],
    providers: [
        FeatureService
    ]
})
export class FeatureDetailComponent implements OnInit {
    @Input() feature: Feature;

    constructor(
        private featureService: FeatureService,
        private route: ActivatedRoute,
        private location: Location
    ) { }

    ngOnInit(): void {
      this.route.paramMap
          .switchMap((params: ParamMap) => this.featureService.getFeature(+params.get('id')))
          .subscribe(feature => this.feature = feature);
    }

    onSelect(feature: Feature, state: boolean): void {
        this.feature.enabled = state;
    }

    goBack(): void {
      this.location.back();
    }

    save(): void {
        this.featureService.update(this.feature)
            .then(() => this.goBack());
    }
}
