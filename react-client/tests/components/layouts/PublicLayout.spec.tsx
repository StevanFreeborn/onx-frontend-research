import { render } from '@testing-library/react';
import { PublicLayout } from '../../../src/components/layouts/PublicLayout';

describe('PublicLayout', () => {
  it('should render layout with children', () => {
    const Child = () => <div>Child</div>;
    const { getByTestId, getByText } = render(
      <PublicLayout>
        <Child />
      </PublicLayout>
    );

    const layout = getByTestId('publicLayout');
    const child = getByText('Child');

    expect(layout).toBeInTheDocument();
    expect(child).toBeInTheDocument();
  });
});
