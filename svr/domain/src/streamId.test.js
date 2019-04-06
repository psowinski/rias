import { describe, it } from 'mocha';
import { expect } from 'chai';
import { createStreamId } from './streamId';

describe('StreamId tests', function() {
  it('do not allow to create streamId with empty cotegory', function() {
    expect(createStreamId('', 'id').isError()).to.true;
  });

  it('do not allow to create streamId with empty id', function() {
    expect(createStreamId('category', '').isError()).to.true;
  });

  it('create correct streamId', function() {
    const streamId = createStreamId('abc', '123');
    expect(streamId.isOk()).to.be.true;

    expect(streamId.ok)
      .to.have.property('category')
      .equal('abc');

    expect(streamId.ok)
      .to.have.property('id')
      .equal('123');
  });

  it('stringify to correct format', function() {
    expect(createStreamId('123', 'abc').ok.toString()).to.be.equal('123-abc');
  });
});
