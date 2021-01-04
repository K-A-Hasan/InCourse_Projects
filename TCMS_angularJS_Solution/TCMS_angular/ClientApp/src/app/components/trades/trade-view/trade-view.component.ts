import { Component, OnInit, ViewChild, QueryList, ViewChildren, ChangeDetectorRef } from '@angular/core';
import { TradeService } from 'src/app/services/trade.service';
import { Trade } from 'src/app/models/trade';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { throwError } from 'rxjs';
import { Course } from 'src/app/models/course';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent } from '../../common/delete-dialog/delete-dialog.component';
import { NotifyService } from 'src/app/services/notify.service';

@Component({
  selector: 'app-trade-view',
  templateUrl: './trade-view.component.html',
  styleUrls: ['./trade-view.component.css'],
  styles: [
    `
    :host {
        display:block;
        width:100%;
    }`
  ],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ]
})
export class TradeViewComponent implements OnInit {
  @ViewChild('outerSort', { static: true }) sort: MatSort;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort>;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<Course>>;
  @ViewChild(MatPaginator, { static: false }) paginator;
  //set paginator(value: MatPaginator) {
  //  this.dataSource.paginator = value;
  //}
  //dataSource: MatTableDataSource<User>; //
  dataSource: MatTableDataSource<Trade>;
  //usersData: User[] = []; //
  tradesData: Trade[] = [];
  columnsToDisplay = ['select', 'TradeName', 'Description', 'FemaleAllowed', 'actions']; //['select', 'name', 'email', 'phone', 'actions'];
  innerDisplayedColumns = ['CourseName', 'Duration', 'actions']; //['street', 'zipCode', 'city', 'actions']; //
  expandedElement: Trade | null; //User | null; //
  constructor(
    private tradeService: TradeService,
    private notifyService: NotifyService,
    private deleteDialog: MatDialog,
    private cd: ChangeDetectorRef
  ) { }
  initTable(data:Trade[]) {
    this.dataSource = new MatTableDataSource(data);
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }
  confirmDelete(item: Trade) {
    let dialogRef = this.deleteDialog.open(DeleteDialogComponent, {
      width: '450px'
    });

    dialogRef.afterClosed().subscribe(
      result => {
        if (result) {
          this.tradeService.delete(item.TradeId)
            .subscribe(x => {
              this.dataSource.data = this.dataSource.data.filter(data => data.TradeId != item.TradeId);
              this.notifyService.message("Data deleted.", ["DISMISS"])
            });
        }
        else { console.log("let it go"); }
      }
    );
  }
  confirmDeleteCourse(item: Course) {
    console.log(this.expandedElement);
    console.log(item);
    let dialogRef = this.deleteDialog.open(DeleteDialogComponent, {
      width: '450px'
    });

    dialogRef.afterClosed().subscribe(
      result => {
        if (result) {
          this.tradeService.deleteCourse(item.CourseId)
            .subscribe(x => {
              let ds = this.expandedElement.Courses as MatTableDataSource<Course>;
              ds.data = ds.data.filter(data => data.CourseId != item.CourseId);
              this.notifyService.message("Data deleted.", ["DISMISS"])
            });
        }
        else { console.log("let it go"); }
      }
    );
  }
  ngOnInit() {

   
    this.tradeService.getWithCourse()
      .subscribe(x => {
        this.tradesData = x;
        //USERS.forEach(user => {
        //  if (user.addresses && Array.isArray(user.addresses) && user.addresses.length) {
        //    this.usersData = [...this.usersData, { ...user, addresses: new MatTableDataSource(user.addresses) }];
        //  } else {
        //    this.usersData = [...this.usersData, user];
        //  }
        //});
        this.tradesData.forEach(trade => {
          if ((trade.Courses as Course[]).length == 0) trade.Courses = null;
          if (trade.Courses && Array.isArray(trade.Courses) && trade.Courses.length) {
            trade.Courses = new MatTableDataSource(trade.Courses);
          } 
        });
        console.log(this.tradesData);
        this.initTable(this.tradesData);
       
        

      }, err => {
        console.log(err);
        return throwError(err);
      })
    
  }

  toggleRow(element: Trade) {
    element.Courses && (element.Courses as MatTableDataSource<Course>).data.length ? (this.expandedElement = this.expandedElement === element ? null : element) : null;
    this.cd.detectChanges();
    this.innerTables.forEach((table, index) => (table.dataSource as MatTableDataSource<Course>).sort = this.innerSort.toArray()[index]);
  }

  applyFilter(filterValue: string) {
    //this.innerTables.forEach((table, index) => (table.dataSource as MatTableDataSource<Address>).filter = filterValue.trim().toLowerCase());
    this.dataSource.filter = filterValue.trim().toLowerCase()
  }

}
