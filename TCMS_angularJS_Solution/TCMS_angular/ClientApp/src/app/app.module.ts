import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppNavComponent } from './app-nav/app-nav.component';
import { LayoutModule } from '@angular/cdk/layout';

import { HomeComponent } from './components/home/home.component';
import { MatIncludeModule } from './shared/common/mat-include/mat-include.module';
import { HttpClientModule } from '@angular/common/http';
import { TradeService } from './services/trade.service';
import { CourseService } from './services/course.service';
import { TradeViewComponent } from './components/trades/trade-view/trade-view.component';
import { NgMaterialMultilevelMenuModule } from 'ng-material-multilevel-menu';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CourseViewComponent } from './components/courses/course-view/course-view.component';
import { CourseCreateComponent } from './components/courses/course-create/course-create.component';
import { CourseEditComponent } from './components/courses/course-edit/course-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DeleteDialogComponent } from './components/common/delete-dialog/delete-dialog.component';
import { TradeCreateComponent } from './components/trades/trade-create/trade-create.component';
import { TradeEditComponent } from './components/trades/trade-edit/trade-edit.component';
import { NotifyService } from './services/notify.service';

@NgModule({
  declarations: [
    AppComponent,
    AppNavComponent,
    HomeComponent,
    TradeViewComponent,
    CourseViewComponent,
    CourseCreateComponent,
    CourseEditComponent,
    DeleteDialogComponent,
    TradeCreateComponent,
    TradeEditComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    LayoutModule,
    MatIncludeModule,
    HttpClientModule,
    NgMaterialMultilevelMenuModule
  ],
  entryComponents: [DeleteDialogComponent],
  providers: [TradeService, CourseService, NotifyService],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
