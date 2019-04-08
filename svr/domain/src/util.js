export function isNonEmptyString(str) {
  return str && typeof str === 'string';
}

export function isValidDate(date) {
  return (
    date &&
    Object.prototype.toString.call(date) === '[object Date]' &&
    !isNaN(date)
  );
}
