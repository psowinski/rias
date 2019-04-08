import { Record } from 'immutable';

const EvnBookOpened = new Record(
  {
    name: '',
    openDate: undefined
  },
  'EvnBookOpened'
);

export function createEvnBookOpened(data) {
  return new EvnBookOpened(data);
}
