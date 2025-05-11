import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UploadFormComponent } from "./upload-form/upload-form.component";
import { ResultsViewComponent } from "./results-view/results-view.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, UploadFormComponent, ResultsViewComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
}
