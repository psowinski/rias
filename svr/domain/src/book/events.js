import { Record } from 'immutable';

const EvnBookOpened = new Record(
  {
    version: 0,
    name: '',
    openDate: new Date('2001-01-01')
  },
  'EvnBookOpened'
);

export function createEvnBookOpened(state, command) {
  return new EvnBookOpened({
    name: command.name,
    openDate: command.openDate,
    version: state.version
  });
}
