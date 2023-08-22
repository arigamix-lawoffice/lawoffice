import { observer } from 'mobx-react';
import { LawCaseHeaderViewModel } from './lawCaseHeaderViewModel';
import { FC } from 'react';

/**
 * Properties of the title of the "Case" card
 */
type LawCaseHeaderProps = {
  viewModel: LawCaseHeaderViewModel;
};

/**
 * Head html
 */
export const LawCaseHeader: FC<LawCaseHeaderProps> = observer(({ viewModel }) => {
  return (
    <div
      style={{
        margin: '5px 12px',
        padding: '5px',
        backgroundColor: 'transparent',
        fontSize: '20px'
      }}
    >
      <span style={{ fontWeight: 'bold' }}>{viewModel.title}</span> <span>{viewModel.number}</span>{' '}
      <span
        style={{
          WebkitBorderRadius: '15px',
          backgroundColor: 'aliceblue',
          marginLeft: '15px',
          visibility: !viewModel.categoryIcon || viewModel.categoryIcon.length === 0 ? 'hidden' : 'visible'
        }}
      >
        <img
          style={{
            marginLeft: '10px',
            paddingBottom: '5px'
          }}
          src={viewModel.categoryIcon}
        />
        <span
          style={{
            fontSize: '14px',
            marginRight: '10px',
            marginLeft: '5px',
            paddingBottom: '5px',
            display: 'inline-block',
            verticalAlign: 'middle'
          }}
        >
          {viewModel.category}
        </span>
      </span>
    </div>
  );
});
