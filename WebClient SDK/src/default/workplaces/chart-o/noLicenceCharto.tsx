import React from 'react';
import styled from 'styled-components';
import { LocalizationManager } from 'tessa/localization';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { BaseContentItem, ContentPlaceArea, ContentPlaceOrder } from 'tessa/ui/views/content';

const NoChartsContainer = styled.div`
  width: 100%;
  height: 100px
  display: flex;
  align-items: center;
  text-align: center;
  > div {
    flex: 1;
  }
`;

export class NoLicenseChartoViewViewModel extends BaseContentItem {
  constructor(
    viewComponent: IWorkplaceViewComponent,
    area: ContentPlaceArea = ContentPlaceArea.ContentPanel,
    order: number = ContentPlaceOrder.BeforeAll
  ) {
    super(viewComponent, area, order);
  }
}

export function NoLicenseCharto() {
  return (
    <NoChartsContainer>
      <div>{LocalizationManager.instance.localize('$Views_Charting_LicenseNotFound')}</div>
    </NoChartsContainer>
  );
}
