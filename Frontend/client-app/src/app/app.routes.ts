import { Routes } from '@angular/router';
import { CalculatorComponent } from './calculator/calculator.component';

export const routes: Routes = [
  { path: '', redirectTo: 'api', pathMatch: 'full' },
  { path: 'services', component: CalculatorComponent },
  { path: 'api', redirectTo: '', pathMatch: 'full' },
  { path: '**', redirectTo: '' }
];
