import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HeroService } from './_services/hero.service';
import { Hero } from './_models/hero';

@Component({
  selector: 'app-frontpage',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <p>frontpage works!</p>
    <div *ngFor="let hero of heroes">
      <h2>{{ hero.heroName | uppercase }}</h2>
      <p>{{ hero.realName }} {{ hero.debutYear }}</p>
    </div>
  `,
  styles: [
  ]
})
export class FrontpageComponent {
  heroes: Hero[] = [];

  constructor(private heroService: HeroService) { }

  ngOnInit(): void {
    this.heroService.getAll()
      .subscribe(x => this.heroes = x);
  }

}
