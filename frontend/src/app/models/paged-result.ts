export class PagedResult<T> {
  items: T[];
  pageNumber: number;
  pageSize: number;
  totalRecordNumber: number;

  constructor(init: Partial<PagedResult<T>>) {
    Object.assign(this, init);
  }
}