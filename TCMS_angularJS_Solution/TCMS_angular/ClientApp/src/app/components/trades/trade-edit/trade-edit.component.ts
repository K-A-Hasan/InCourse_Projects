import { Component, OnInit } from '@angular/core';
import { TradeService } from 'src/app/services/trade.service';
import { NotifyService } from 'src/app/services/notify.service';
import { Trade } from 'src/app/models/trade';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { Course } from 'src/app/models/course';
import { throwError } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-trade-edit',
  templateUrl: './trade-edit.component.html',
  styleUrls: ['./trade-edit.component.css'],
  styles: [
    `
    :host {
        display:block;
        width:100%;
    }`
  ]
})
export class TradeEditComponent implements OnInit {
  trade: Trade = null;
  tradeForm: FormGroup = new FormGroup({
    TradeName: new FormControl('', Validators.required),
    Description: new FormControl('', Validators.required),
    FemaleAllowed: new FormControl(''),
    Courses: new FormArray([]),
    NewCourses: new FormArray([])
  });
  removeItem(index: number) {
    (this.trade.Courses as Course[]).splice(index, 1);
  }
  onSubmit() {
    if (this.tradeForm.controls.TradeName.invalid ||
      this.tradeForm.controls.Description.invalid ||
      (this.trade.Courses as Course[]).length == 0
    ) return;
    this.trade.TradeName = this.tradeForm.controls.TradeName.value;
    this.trade.Description = this.tradeForm.controls.Description.value;
    this.trade.FemaleAllowed = this.tradeForm.controls.FemaleAllowed.value;
    let i = 0;
    for (let x of this.CourseArray.controls) {
      console.log();
      (this.trade.Courses as Course[])[i].Duration = x.get("Duration").value;
      (this.trade.Courses as Course[])[i].CourseName = x.get("CourseName").value;
      i++;
    }
    console.log(this.trade);

    this.tradeService.update(this.trade)
      .subscribe(x => {
        this.notifyService.message("Data Saved successfully", 'DISMISS');
        
        this.tradeForm.markAsUntouched();
        this.tradeForm.markAsPristine();
        this.trade = new Trade();
       
        
      }, err => {
        this.notifyService.message("Could not insert data.", 'DISMISS');
        return throwError(err);
      })
  }
  addCourseForm() {
    (this.tradeForm.get('NewCourses') as FormArray).push(
      new FormGroup({
        CourseName: new FormControl('', Validators.required),
        Duration: new FormControl('', Validators.required)
      }));
  }
  addCourseToTrade() {
    console.log(this.NewCourseArray.controls[0].value);
    let course = new Course();
    Object.assign(course, this.NewCourseArray.controls[0].value);
    (this.trade.Courses as Course[]).push(course);
    (this.tradeForm.get('Courses') as FormArray).push(
      new FormGroup({
        CourseName: new FormControl(course.CourseName, Validators.required),
        Duration: new FormControl(course.Duration, Validators.required)
      }));
    this.NewCourseArray.controls[0].reset({});
    this.NewCourseArray.controls[0].markAsPristine();
    this.NewCourseArray.controls[0].markAsUntouched();
  }
  get CourseArray() {
    return this.tradeForm.get("Courses") as FormArray;
  }
  get NewCourseArray() {
    return this.tradeForm.get("NewCourses") as FormArray;
  }
  initForm() {
    this.tradeForm.setValue({
      TradeName: this.trade.TradeName,
      Description: this.trade.Description,
      FemaleAllowed: this.trade.FemaleAllowed,
      Courses: [],
      NewCourses: []
    });
    for (let x of this.trade.Courses as Course[]) {
      (this.tradeForm.get('Courses') as FormArray).push(
        new FormGroup({
          CourseName: new FormControl(x.CourseName, Validators.required),
          Duration: new FormControl(x.Duration, Validators.required)
        }));
    }
    this.addCourseForm();
  }
  removeCourseItem(index: number) {
    this.CourseArray.removeAt(index);
  }
  constructor(
    private tradeService: TradeService,
    private notifyService: NotifyService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    let id = this.activatedRoute.snapshot.params.id;
    console.log(id);
    this.tradeService.getWithCourseById(id)
      .subscribe(x => {
        console.log(x);
        this.trade = x;
        this.initForm();
      }, err => {
        this.notifyService.message("Failed to fetch trade data.", 'DISMISS');
        return throwError(err);
      })
   
  }
 

}
