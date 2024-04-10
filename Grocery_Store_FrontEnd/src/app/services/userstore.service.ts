import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserstoreService {
  private fullname$ = new BehaviorSubject<string>("");
  private isAdmin$ = new BehaviorSubject<boolean>(false);
  private id$ = new BehaviorSubject<string>("");

  constructor() { }

  public getRoleFromStore(){
    return this.isAdmin$.asObservable();
  }

  public setRoleForStore(isAdmin:boolean){
    this.isAdmin$.next(isAdmin);
  }

  public getFullNameFromStore(){
    return this.fullname$.asObservable();
  }

  public setFullNameForStore(fullname:string){
    this.fullname$.next(fullname);
  }

  public setIdForToken(id:string){
    this.id$.next(id);
  }

  public getFullIdFromStore(){
    return this.id$.asObservable();
  }
}
