import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HeroService } from '../_services/hero.service';
import { Hero, ResetHero } from '../_models/hero';

@Component({
  selector: 'app-admin-hero',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-hero.component.html',
  styles: [
  ]
})
export class AdminHeroComponent implements OnInit {
  message: string = '';
  heroes: Hero[] = [];
  hero: Hero = ResetHero();

  constructor(private heroService: HeroService) { }
  ngOnInit(): void {
    this.heroService.getAll()
      .subscribe(x => this.heroes = x);
  }

  edit(hero: Hero): void {
    Object.assign(this.hero, hero);
  }

  delete(hero: Hero): void {
    if (confirm('Er du sikker på du vil slette?')) {
      this.heroService.delete(hero.id)
        .subscribe(() => {
          this.heroes = this.heroes.filter(x => x.id != hero.id)
        });
    }
  }

  cancel(): void {
    this.message = '';
    this.hero = ResetHero();
  }

  save(): void {
    this.message = '';
    if (this.hero.id == 0) {
      // create
      this.heroService.create(this.hero)
        .subscribe({
          next: (x) => {
            this.heroes.push(x);
            this.hero = ResetHero();
          },
          error: (err) => {
            console.log(err);
            this.message = Object.values(err.error.errors).join(', ');
          }
        });
    } else {
      // update
      this.heroService.update(this.hero)
        .subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(', ');
          },
          complete: () => {
            this.heroService.getAll().subscribe(x => this.heroes = x);
            this.hero = ResetHero();
          }
        });
    }
  }
}
