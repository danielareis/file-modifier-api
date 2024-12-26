import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss'],
  imports: [CommonModule, ReactiveFormsModule]
})
export class FileUploadComponent {
  uploadForm: FormGroup;
  selectedFile: File | null = null;

  constructor(private http: HttpClient, private formBuilder: FormBuilder) {
    this.uploadForm = this.formBuilder.group({
      file: ['']
    });
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
    this.uploadForm.patchValue({
      file: this.selectedFile
    });
  }

  onSubmit() {
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append('file', this.selectedFile, this.selectedFile.name);

      this.http.post('https://localhost:5056/api/file/upload', formData, { responseType: 'blob' })
        .subscribe(response => {
          const blob = new Blob([response], { type: 'application/octet-stream' });
          const url = window.URL.createObjectURL(blob);
          const a = document.createElement('a');
          a.href = url;
          a.download = 'mutated_' + this.selectedFile?.name;
          a.click();
          window.URL.revokeObjectURL(url);
        });
    }
  }
}