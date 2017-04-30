import {Component, OnInit, ViewChild} from '@angular/core';
import {Http, RequestOptions} from "@angular/http";
import {WeatherData} from "./weatherData.model";
import {BaseChartDirective} from "ng2-charts";
import {NgForm} from "@angular/forms";
import {min} from "rxjs/operator/min";


@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

    isBusy: boolean;
    chartData: any;
    baseUri: string = "http://localhost/api";
    rows: WeatherData[] = [];
    columns: any[] = [
        {prop: 'Time', flexGrow: 1},
        {prop: 'Temperature', flexGrow: 1},
        {prop: 'Humidity', flexGrow: 1}
    ];

    constructor(private http: Http) {

    }

    ngOnInit(): void {
        this.isBusy = true;
        this.chartData = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [
                {
                    label: 'Temperature',
                    data: [65, 59, 80, 81, 56, 55, 40]
                },
                {
                    label: 'Humidity',
                    data: [28, 48, 40, 19, 86, 27, 90]
                }
            ]

        };
        this.reload();
    }

    reload(): void {
        this.http.get(this.baseUri + "/getalldata.php").map(response => (<WeatherData[]>response.json())).subscribe(
            rows => {

                this.rows = rows;
                this.updateChart();
                this.forecast();
            });
    }


    updateChart(): void {
        let _newData = {
            labels: [],
            datasets: [{
                label: 'Temperature',
                data: [],
                backgroundColor: "rgba(255,0,0,0.5)"
            }, {
                label: 'Humidity',
                data: [],
                backgroundColor: "rgba(0,0,255,0.5)"
            }]
        }
        for (let i = Math.min(this.rows.length, 29) - 1; i >= 0; i--) {
            _newData.labels.push(this.rows[i].Time);
            _newData.datasets[0].data.push(this.rows[i].Temperature);
            _newData.datasets[1].data.push(this.rows[i].Humidity);
        }
        this.chartData = _newData;
    }

    forecast(): void {
        this.http.get(this.baseUri + "/getForecast.php").map(response => (<WeatherData>response.json())).subscribe(
            row => {

                // let n = this.chartData.labels.length;
                this.chartData.labels.push(row.Time);
                this.chartData.datasets[0].data.push(row.Temperature);
                this.chartData.datasets[1].data.push(row.Humidity);
                this.isBusy = false;
            }
        );
    }

    add(value: any) {
        this.isBusy = true;

        this.http.post(this.baseUri + "/adddata.php", value).subscribe(response => {
            this.reload();
        });
    }

}
