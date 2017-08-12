import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Feature } from './feature';

@Injectable()
export class FeatureService {

    private featuresUrl = 'http://localhost:11713/api/features';  // URL to web api

    constructor(private http: Http) { }

    getFeatures(): Promise<Feature[]> {
        return this.http.get(this.featuresUrl)
            .toPromise()
            .then(response => response.json() as Feature[])
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}
