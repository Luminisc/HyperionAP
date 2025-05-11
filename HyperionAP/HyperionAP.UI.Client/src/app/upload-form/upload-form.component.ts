import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiModule } from '../api.module';
import { DemoClient, FileParameter } from '../api/api.generated';

@Component({
  selector: 'app-upload-form',
  imports: [FormsModule, ApiModule],
  templateUrl: './upload-form.component.html',
  styleUrl: './upload-form.component.scss'
})
export class UploadFormComponent {
  selectedFile?: File;
  resultingId: string = '';

  constructor(private demoClient: DemoClient) { }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  upload() {
    if (!this.selectedFile)
      return;

    let fileParameter: FileParameter = { data: this.selectedFile, fileName: this.selectedFile.name };
    this.demoClient.upload(fileParameter)
      .subscribe(fileInfo => { this.resultingId = fileInfo.fileId })
  }
}
