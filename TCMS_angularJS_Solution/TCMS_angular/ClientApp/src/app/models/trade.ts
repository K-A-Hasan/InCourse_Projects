import { Course } from './course';
import { MatTableDataSource } from '@angular/material/table';

export class Trade {
  constructor(
    public TradeId?: number,
    public TradeName?: string,
    public Description?: string,
    public FemaleAllowed?: boolean,
    public Courses?: Course[] | MatTableDataSource<Course>
  ) { }
}
export interface TradeDataSource {
  TradeId?: number;
  TradeName?: string;
  FemaleAllowed?: boolean;
  Courses?: MatTableDataSource<Course>;
}
