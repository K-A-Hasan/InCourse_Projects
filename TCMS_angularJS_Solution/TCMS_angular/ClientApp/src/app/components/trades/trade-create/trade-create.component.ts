import { Component, OnInit } from '@angular/core';
import { TradeService } from 'src/app/services/trade.service';
import { NotifyService } from 'src/app/services/notify.service';
import { Trade } from 'src/app/models/trade';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { Course } from 'src/app/models/course';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-trade-create',
  templateUrl: './trade-create.component.html',
  styleUrls: ['./trade-create.component.css'],
  styles: [
    `
    :host {
        display:block;
        width:100%;
    }`
  ]
})
export class TradeCreateComponent implements OnInit {
  trade: Trade = new Trade();
  tradeForm: FormGroup = new FormGroup({
    TradeName: new FormControl('', Validators.required),
    Description: new FormControl('', Validators.required),
    FemaleAllowed: new FormControl(''),
    Courses: new FormArray([])
  });
  removeItem(index: number) {
    (this.trade.Courses as Course[]).splice(index, 1);
  }
  onSubmit() {
    console.log(this.isDisabled);
    if (this.tradeForm.controls.TradeName.invalid ||
      this.tradeForm.controls.Description.invalid
    )
    {
      this.notifyService.message("Errors in form", 'DISMISS')
      return;
    }
    if ((this.trade.Courses as Course[]).length == 0)
    {
      this.notifyService.message("No course added.", 'DISMISS')
      return;
    }
    this.trade.TradeName = this.tradeForm.controls.TradeName.value;
    this.trade.Description = this.tradeForm.controls.Description.value;
    this.trade.FemaleAllowed = this.tradeForm.controls.FemaleAllowed.value;
    this.trade.TradeId = 0;
    console.log(this.trade);

    this.tradeService.insert(this.trade)
      .subscribe(x => {
        this.notifyService.message("Data Saved successfully", 'DISMISS');
        this.tradeForm.reset({});
        this.tradeForm.markAsUntouched();
        this.tradeForm.markAsPristine();
        this.trade = new Trade();
        this.trade.Courses = [];
      }, err => {
        this.notifyService.message("Could not insert data.", 'DISMISS');
        return throwError(err);
      })
  }
  addCourseForm() {
    (this.tradeForm.get('Courses') as FormArray).push(
      new FormGroup({
        CourseName: new FormControl('', Validators.required),
        Duration: new FormControl('', Validators.required)
      }));
  }
  addCourseToTrade() {
    console.log(this.CourseArray.controls[0].value);
    let course = new Course();
    Object.assign(course, this.CourseArray.controls[0].value);
    (this.trade.Courses as Course[]).push(course);
    this.CourseArray.controls[0].reset({});
    this.CourseArray.controls[0].markAsPristine();
    this.CourseArray.controls[0].markAsUntouched();
  }
  get CourseArray() {
    return this.tradeForm.get("Courses") as FormArray;
  }
  get courseLength() {
    return (this.trade.Courses ? (this.trade.Courses as Course[]).length : 0)
  }
  get isDisabled() {
    return this.tradeForm.controls.TradeName.invalid ||
      this.tradeForm.controls.Description.invalid || (this.trade.Courses as Course[]).length == 0;
  }
  constructor(
    private tradeService: TradeService,
    private notifyService: NotifyService
  ) { }

  ngOnInit(): void {
    this.trade.Courses = [];
    this.addCourseForm();
  }

}
