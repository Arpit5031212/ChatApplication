import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { SignalrServiceService } from './shared/SignalrService/signalr-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  constructor(private signalrService: SignalrServiceService) { }

  ngOnInit() {
    this.signalrService.startConnection();
  }
}
