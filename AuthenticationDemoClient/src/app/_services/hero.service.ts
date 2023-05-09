import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Hero } from '../_models/hero';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HeroService {

  private apiUrl = environment.apiUrl + 'Hero';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Hero[]> {
    return this.http.get<Hero[]>(this.apiUrl);
  }
  getById(superHeroId: number): Observable<Hero> {
    return this.http.get<Hero>(`${this.apiUrl}/${superHeroId}`);
  }
  create(superHero: Hero): Observable<Hero> {
    return this.http.post<Hero>(this.apiUrl, superHero);
  }
  update(superHero: Hero): Observable<Hero> {
    return this.http.put<Hero>(`${this.apiUrl}/${superHero.id}`, superHero);
  }
  delete(superHeroId: number): Observable<Hero> {
    return this.http.delete<Hero>(`${this.apiUrl}/${superHeroId}`);
  }
}
