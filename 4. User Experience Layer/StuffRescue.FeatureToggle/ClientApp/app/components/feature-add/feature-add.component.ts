import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import { Feature } from '../shared/feature';
import { FeatureService } from '../shared/feature.service';
import { NotificationService } from '../shared/notification/service/notification.service'

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
        private notificationService: NotificationService,
        private location: Location) { }

    public options = {
        position: ["top", "right"],
        timeOut: 5000,
        lastOnBottom: true,
    };


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
        this.featureService.create(name, state)
            .then(() => {
                this.notificationService.success(
                    'Success!',
                    name + ' created',
                    {
                        showProgressBar: true,
                        pauseOnHover: false,
                        clickToClose: false,
                        maxLength: 30
                    }
                );
                //TODO Fix Timeout
                setTimeout(
                    this.goBack()
                , 5000);
                
            });
    }
}
