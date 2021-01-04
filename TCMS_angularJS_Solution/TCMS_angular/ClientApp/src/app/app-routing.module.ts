import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { TradeViewComponent } from './components/trades/trade-view/trade-view.component';
import { CourseCreateComponent } from './components/courses/course-create/course-create.component';
import { CourseEditComponent } from './components/courses/course-edit/course-edit.component';
import { TradeCreateComponent } from './components/trades/trade-create/trade-create.component';
import { TradeEditComponent } from './components/trades/trade-edit/trade-edit.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'trades', component: TradeViewComponent },
  { path: 'createCourse', component: CourseCreateComponent },
  { path: 'editCourse/:id', component: CourseEditComponent },
  { path: 'createTrade', component: TradeCreateComponent },
  { path: 'editTrade/:id', component: TradeEditComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
