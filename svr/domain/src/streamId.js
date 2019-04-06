import { Record } from 'immutable';
import { error, ok } from 'riasfp';

const StreamId = new Record(
  {
    category: 'unknown',
    id: 'unknown'
  },
  'StreamId'
);

StreamId.prototype.toString = function() {
  return `${this.category}-${this.id}`;
};

export function createStreamId(category, id) {
  if (!category) return error('empty category');
  if (!id) return error('empty id');
  return ok(new StreamId({ category: category, id: id }));
}
