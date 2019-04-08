import { describe, it } from 'mocha';
import { expect } from 'chai';
import { createCmdOpenBook } from './commands';

const anyValidDate = new Date('2001-02-03');

describe('Book commands tests', function() {
  it('should create open book command on valid data', function() {
    const expected = { name: 'abc', openDate: anyValidDate };
    const cmd = createCmdOpenBook(expected).ok;
    expect(cmd).to.include(expected);
  });

  it('should fail on invalid name', function() {
    const result = createCmdOpenBook({ name: '', openDate: anyValidDate });
    expect(result.isError()).to.be.true;
  });

  it('should fail on nonstring name', function() {
    const result = createCmdOpenBook({ name: 123, openDate: anyValidDate });
    expect(result.isError()).to.be.true;
  });

  it('should fail on invalid date', function() {
    const result = createCmdOpenBook({
      name: 'abc',
      openDate: new Date('abc')
    });
    expect(result.isError()).to.be.true;
  });
});
