import {Component,ViewEncapsulation} from '@angular/core';
import DataSource from 'devextreme/data/data_source';
import {DxDataGridModule} from 'devextreme-angular';
import * as AspNetData from "devextreme-aspnet-data-nojquery";

@Component(
{
 templateUrl:'./dxGrid.component.html',
 styleUrls:['./dxGrid.component.css'],
 encapsulation:ViewEncapsulation.None
}
)

export class DxGridComponent
{
genderData:any;
jobTitlesData:any;
companyData:any
url:string;
dataSource:any;
masterDetailsDataSource:any;


constructor() {
        this.url = "http://localhost:22742/api/services/app/dxdatagrid";
        
        this.dataSource = AspNetData.createStore({
            key: "id",
            loadUrl: this.url + "/GetDummyData",
            insertUrl: this.url + "/CreatePerson",
            updateUrl: this.url + "/UpdatePerson",
            deleteUrl: this.url + "/DeletePerson",
            onBeforeSend: function(method, ajaxOptions) {
                ajaxOptions.xhrFields = { withCredentials: true };
            }
        });

        this.companyData = AspNetData.createStore({
            key: "value",
            loadUrl: this.url + "/GetCompanies",
            onBeforeSend: function(method, ajaxOptions) {
                ajaxOptions.xhrFields = { withCredentials: true };
            }
        });

        this.genderData = AspNetData.createStore({
            key: "value",
            loadUrl: this.url + "/GetGender",
            onBeforeSend: function(method, ajaxOptions) {
                ajaxOptions.xhrFields = { withCredentials: true };
            }
        });

    this.jobTitlesData = AspNetData.createStore({
            key: "value",
            loadUrl: this.url + "/GetJobTitles",
            onBeforeSend: function(method, ajaxOptions) {
                ajaxOptions.xhrFields = { withCredentials: true };
            }
        });
    }


  getMasterDetailGridDataSource(id: number) : any {   
        return {
            store: AspNetData.createStore({
                loadUrl: this.url + '/OrderDetails',
                loadParams: { orderID : id },
                onBeforeSend: function(method, ajaxOptions) {
                    ajaxOptions.xhrFields = { withCredentials: true };
                }
            })
        };
    }


}