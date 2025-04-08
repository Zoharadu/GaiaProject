import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css'],
  imports: [FormsModule, HttpClientModule,CommonModule]
})
export class CalculatorComponent implements OnInit {
  a: number = 0;
  b: number = 0;
  operator: string = '';
  result: number | null = null;
  operations: string[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getOperations();
  }

  getOperations() {
    this.http.get<string[]>('https://localhost:7187/api/GaiaProject/supported-operations')
      .subscribe({
        next: ops => this.operations = ops,
        error: err => console.error('שגיאה בהבאת הפעולות:', err)
      });
  }

  calculate() {
    const request = {
      a: this.a,
      b: this.b,
      operator: this.operator
    };

    this.http.post<{ result: number }>('https://localhost:7187/api/GaiaProject/calculate', request)
      .subscribe({
        next: res => this.result = res.result,
        error: err => console.error('שגיאה בחישוב:', err)
      });
  }
}
