import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface OperationRequest {
  a: number;
  b: number;
  operator: string;
  result?: number;
}

interface OperationResult {
  result: number;
  lastThreeOperations: OperationRequest[];
  operationsCountThisMonth: number;
}

@Component({
  selector: 'app-calculator',
  standalone: true,
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css'],
  imports: [CommonModule, FormsModule, HttpClientModule]
})
export class CalculatorComponent implements OnInit {
  a: number = 0;
  b: number = 0;
  operator: string = '';
  operations: string[] = [];
  operationResult: OperationResult | null = null;

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
    const request: OperationRequest = {
      a: this.a,
      b: this.b,
      operator: this.operator
    };

    this.http.post<OperationResult>('https://localhost:7187/api/GaiaProject/calculate', request)
      .subscribe({
        next: res => this.operationResult = res,
        error: err => console.error('שגיאה בחישוב:', err)
      });
  }
}
