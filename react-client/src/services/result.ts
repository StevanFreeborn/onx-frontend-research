export type Result<T, E = Error> =
  | { isFailed: false; isSuccess: true; value: T }
  | { isFailed: true; isSuccess: false; error: E };

export const Result = {
  success<T>(value: T): Result<T> {
    return {
      isFailed: false,
      isSuccess: true,
      value,
    };
  },
  failure<E>(error: E): Result<never, E> {
    return {
      isFailed: true,
      isSuccess: false,
      error,
    };
  },
};
