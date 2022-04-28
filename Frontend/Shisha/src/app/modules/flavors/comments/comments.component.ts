import { Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { of, Subscription } from 'rxjs';
import { CommentsService } from 'src/app/services/comments.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent implements OnInit, OnDestroy {

  public subscription: Subscription | any;
  public comments: Comment[] | any;
  public users: any;
  public focused : boolean;
  public id: number | undefined;
  public token: string | undefined;
  public commentForm: FormGroup = new FormGroup({
    text: new FormControl(''),
    flavorId: new FormControl('')
  });

  constructor(
    private route: ActivatedRoute,
    private commentsService: CommentsService,
    private usersService: UsersService,
    private router: Router
  ) {
    this.focused = false;
  }

  ngOnInit(): void {
    var auxtoken = localStorage.getItem('Token');
    if(auxtoken != undefined){
      this.token = auxtoken;
    }
    this.subscription = this.route.params.subscribe(params => {
      this.id = +params['id'];
      this.getAllComments(this.id);
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  get email(): AbstractControl | null {
    return this.commentForm.get('text');
  }

  public getAllComments(id: any): void {
    this.commentsService.getComments(id).subscribe(result => {
      this.comments = result;
      console.log(this.comments)
    });
  }

  public postComment(): void {
    this.commentForm.patchValue({flavorId: this.id})
    const body = this.commentForm.value;
    console.log(body);
    var response = this.commentsService.postComment(body).subscribe(result => {
      this.focused = false;
      this.getAllComments(this.id);
    });
  }

  public goToLogin(): void{
    this.router.navigate(['auth/login']);
  }

}
