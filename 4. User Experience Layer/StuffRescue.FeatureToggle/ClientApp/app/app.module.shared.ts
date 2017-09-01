import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NotificationModule } from './components/shared/notification/notification.module';

import { AppComponent } from './components/app/app.component'
import { HomeComponent } from './components/home/home.component';
import { NavigatorComponent } from './components/shared/navigator/navigator.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { FeaturesComponent } from './components/features/features.component';
import { FeatureDetailComponent } from './components/feature-detail/feature-detail.component';
import { FeatureAddComponent } from './components/feature-add/feature-add.component';

import { FeatureService } from './components/shared/feature.service';

export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        HomeComponent,
        NavigatorComponent,
        DashboardComponent,
        FeaturesComponent,
        FeatureDetailComponent,
        FeatureAddComponent
    ],
    imports: [
        FormsModule, 
        BrowserAnimationsModule,
        NotificationModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
            { path: 'dashboard', component: DashboardComponent },
            { path: 'detail/:id', component: FeatureDetailComponent },
            { path: 'add', component: FeatureAddComponent },
            { path: 'features', component: FeaturesComponent },
            { path: '**', redirectTo: 'dashboard' },
        ])
    ],
    providers: [
        FeatureService
    ],
};
