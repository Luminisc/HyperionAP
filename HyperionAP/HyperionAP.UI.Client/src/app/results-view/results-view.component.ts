import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { tap } from 'rxjs';
import { ApiModule } from '../api.module';
import { DemoClient } from '../api/api.generated';

@Component({
  selector: 'app-results-view',
  imports: [FormsModule, CommonModule, ApiModule],
  templateUrl: './results-view.component.html',
  styleUrl: './results-view.component.scss'
})
export class ResultsViewComponent {
  bandIndex: number = 10;
  fileId: string = '';
  imageUrl: SafeUrl | null = null;

  constructor(private sanitizer: DomSanitizer, private demoClient: DemoClient) { }

  download() {
    this.demoClient.getImageBand(this.fileId, this.bandIndex)
      .pipe(tap((blob) => {
        const url = URL.createObjectURL(blob.data);
        this.imageUrl = this.sanitizer.bypassSecurityTrustUrl(url);
      }))
      .subscribe();
  }
}
