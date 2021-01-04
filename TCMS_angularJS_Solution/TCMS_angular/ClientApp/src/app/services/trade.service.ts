import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Trade } from '../models/trade';
import { dataUrl } from '../shared/common/constants';
import { Course } from '../models/course';

@Injectable({
  providedIn: 'root'
})
export class TradeService {

  constructor(
    private http: HttpClient
  ) { }
  getWithCourse(): Observable<Trade[]> {
    return this.http.get<Trade[]>(`${dataUrl}/TradeData/TradesWithCourse`);
  }
  getWithCourseById(id: number): Observable<Trade> {
    return this.http.get<Trade>(`${dataUrl}/TradeData/TradesWithCourseById/${id}`);
  }
  insert(t: Trade): Observable<Trade> {
   // console.log(t);
    return this.http.post<Trade>(`${dataUrl}/TradeData/InsertTradesWithCourse`, t);
  }
  update(t: Trade): Observable<Trade> {
    return this.http.put<Trade>(`${dataUrl}/TradeData/UpdateTradesWithCourse/${t.TradeId}`, t);
  }
  delete(id: number): Observable<Trade> {
    return this.http.delete<Trade>(`${dataUrl}/TradeData/DeleteTrade/${id}`);
  }
  deleteCourse(id: number): Observable<Course> {
    return this.http.delete<Course>(`${dataUrl}/CourseData/DeleteCourse/${id}`);
  }
}
