import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-upload-form',
  imports: [FormsModule],
  templateUrl: './upload-form.component.html',
  styleUrl: './upload-form.component.scss'
})
export class UploadFormComponent {
  selectedFile: any;
  resultingId: string = '';

  constructor(private http: HttpClient) { }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  upload() {
    const formData = new FormData();
    formData.append('file', this.selectedFile);
    this.http.post('api/Demo/upload', formData)
      .subscribe((r: any) => { this.resultingId = r.fileId });
  }
}
