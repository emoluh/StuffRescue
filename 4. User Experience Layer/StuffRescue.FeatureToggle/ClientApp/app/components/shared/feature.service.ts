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

    getFeature(id: number): Promise<Feature> {
        const url = `${this.featuresUrl}/${id}`;
        return this.http.get(url)
            .toPromise()
            .then(response => response.json() as Feature)
            .catch(this.handleError);
    }

    private headers = new Headers({ 'Content-Type': 'application/json' });

    update(feature: Feature): Promise<Feature> {
        const url = `${this.featuresUrl}/${feature.featureId}`;
        return this.http
            .put(url, JSON.stringify(feature), { headers: this.headers })
            .toPromise()
            .then(() => feature)
            .catch(this.handleError);
    }

    create(name: string, state: boolean): Promise<Feature> {
        return this.http
            .post(this.featuresUrl, JSON.stringify({ name: name, enabled: state }), { headers: this.headers })
            .toPromise()
            .then(res => res.json() as Feature)
            .catch(this.handleError);
    }

    delete(id: number): Promise<void> {
        const url = `${this.featuresUrl}/${id}`;
        return this.http.delete(url, { headers: this.headers })
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }
}
