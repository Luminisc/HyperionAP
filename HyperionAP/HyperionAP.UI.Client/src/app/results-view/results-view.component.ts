import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { tap } from 'rxjs';

@Component({
  selector: 'app-results-view',
  imports: [FormsModule, CommonModule],
  templateUrl: './results-view.component.html',
  styleUrl: './results-view.component.scss'
})
export class ResultsViewComponent {
  results: any;
  bandIndex: number = 10;
  fileId: string = '';
  imageUrl: SafeUrl | null = null;

  constructor(private http: HttpClient, private sanitizer: DomSanitizer) { }

  download() {
    this.http.get('api/Demo/GetImageBand', { params: { fileId: this.fileId, band: this.bandIndex }, responseType: 'blob' })
      .pipe(tap((blob) => {
        const url = URL.createObjectURL(blob);
        this.imageUrl = this.sanitizer.bypassSecurityTrustUrl(url);
      }))
      .subscribe(data => this.results = data);
  }
}
