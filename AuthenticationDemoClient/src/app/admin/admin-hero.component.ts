import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HeroService } from '../_services/hero.service';
import { Hero, resetHero } from '../_models/hero';

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
  hero: Hero = resetHero();

  // hold styr på hvilke kategorier der skal sendes med til API
  selected: number[] = [];

  // dummy kategori data, checked represeterer om kategorien er valgt eller ej
  categories: any[] = [
    { id: 1, checked: false },
    { id: 2, checked: true },
    { id: 3, checked: false },
    { id: 4, checked: false },
    { id: 5, checked: true },
    { id: 6, checked: false },
    { id: 7, checked: false },
    { id: 8, checked: false }];

  // når en kategori bliver markeret, så opdater selected array med de valgte kategorier
  marked(event: any) {
    let value = parseInt(event.target.value);
    if (this.selected.indexOf(value) == -1) {
      this.selected.push(value);
    } else {
      this.selected.splice(this.selected.indexOf(value), 1);
    }
    this.selected.sort((a, b) => a - b);
    console.log("Seleted IDs ", this.selected);

  }
  constructor(private heroService: HeroService) { }
  ngOnInit(): void {
    // KUN FOR DEMO...
    this.selected = this.categories.filter(x => x.checked == true ? x.id : null).map(x => x.id);
    this.heroService.getAll()
      .subscribe(x => this.heroes = x);
  }

  edit(hero: Hero): void {
    // under edit er det vigtigt at selected array bliver opdateret med de kategorier der er valgt
    this.selected = this.categories.filter(x => x.checked == true ? x.id : null).map(x => x.id);
    this.selected.sort((a, b) => a - b);
    console.log("Seleted IDs ", this.selected);
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
    this.hero = resetHero();
  }

  save(): void {
    this.message = '';
    if (this.hero.id == 0) {
      // create
      // husk at opdatere hero.categories med de valgte kategorier
      // this.hero.categories = this.selected;
      this.heroService.create(this.hero)
        .subscribe({
          next: (x) => {
            this.heroes.push(x);
            this.hero = resetHero();
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
            this.hero = resetHero();
          }
        });
    }
  }
}
