import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationComponent } from './components/notification.component';
import { CoreNotificationComponent } from './components/core-notification.component';
import { MaxPipe } from './pipes/max.pipe';
import { NotificationService } from './service/notification.service';

// Type
export * from './interfaces/notification.type';
export * from './interfaces/options.type';
export * from './interfaces/icons';

export * from './components/notification.component';
export * from './components/core-notification.component';
export * from './pipes/max.pipe';
export * from './service/notification.service';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [
        NotificationComponent,
        CoreNotificationComponent,
        MaxPipe
    ],
    exports: [NotificationComponent]
})
export class NotificationModule {
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: NotificationModule,
            providers: [NotificationService]
        };
    }
}
