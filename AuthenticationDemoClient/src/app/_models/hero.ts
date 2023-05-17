export interface Hero {
  id: number;
  heroName: string;
  realName: string;
  place: string;
  debutYear: number;
}

export function resetHero(): Hero {
  return { id: 0, heroName: '', realName: '', place: '', debutYear: 0 };
}
