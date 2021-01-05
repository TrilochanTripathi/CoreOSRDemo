import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DxGridComponent } from './gridDemo/dxGrid.component';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } }
                ]
            },
			{ path: 'dxGrid', component: DxGridComponent, data: { permission: ''}}
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
